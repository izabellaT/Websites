using Microsoft.AspNetCore.Mvc;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Models.Statistic;

namespace SkateAppWeb.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticService;
        public StatisticsController(IStatisticsService statisticService)
        {
            _statisticService = statisticService;
        }
        // GET: StatisticsController
        public ActionResult Index()
        {
            StatisticVM statistics = new StatisticVM();

            statistics.CountClients = _statisticService.CountClients();
            statistics.CountOrders = _statisticService.CountOrders();
            statistics.CountProducts = _statisticService.CountProducts();
            statistics.CountFavorites = _statisticService.CountFavorites();
            statistics.SumOrders = _statisticService.SumOrders();

            return View(statistics);
        }
    }
}
