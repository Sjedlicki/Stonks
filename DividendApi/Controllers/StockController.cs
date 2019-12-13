using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DividendApi.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DividendApi.Controllers
{
    public class StockController : Controller
    {
        private readonly string _secretToken;
        private HttpClient _client;
        private readonly StockDbContext _context;

        public StockController(IConfiguration configuration, IHttpClientFactory httpClientFactory, StockDbContext context)
        {
            _secretToken = configuration.GetSection("ApiKeys")["SecretKey"];
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://cloud.iexapis.com/stable/");
            _context = context;
        }

        public async Task<IActionResult> Home()
        {
            var summary = await GetPortfolioSummary();
            return View(summary);
        }

        public async Task<IEnumerable<Stock>> GetPortfolioSummary()
        {
            return from stock in await _context.Stock.ToListAsync()
                                          group stock by stock.Symbol into stocked
                                          select new Stock
                                          {
                                              Symbol = stocked.Key,
                                              CompanyName = stocked.Select(x => x.CompanyName).First(),
                                              Shares = stocked.Sum(s => s.Shares),
                                              Price = stocked.Average(p => p.Price)
                                          };            
        }

        public async Task<Dividend> GetDividendInfo(string symbol)
        {
            var response = await _client.GetAsync($"stock/{symbol}/dividends/1m?token={_secretToken}");
            return await response.Content.ReadAsAsync<Dividend>();
        }

        public async Task<Company> GetCompanyInfo(string symbol)
        {
            var response = await _client.GetAsync($"stock/{symbol}/company?token={_secretToken}");
            return await response.Content.ReadAsAsync<Company>();
        }

        public async Task<IActionResult> AddStockToPortfolio(Stock stock)
        {
            Company company = await GetCompanyInfo(stock.Symbol);
            stock.Symbol = company.Symbol;
            stock.CompanyName = company.CompanyName;

            if (ModelState.IsValid)
            {
                _context.Stock.Add(stock);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Home");
        }

        public IActionResult UpdateEntry(Stock stock)
        {
            return View(stock);
        }

        public async Task<IActionResult> UpdatedEntry(Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(stock).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Home");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public async Task<IActionResult> RemoveEntry(Stock stock)
        {
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();

            return RedirectToAction("Home");
        }
    }
}
