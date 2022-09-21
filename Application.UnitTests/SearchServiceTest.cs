using Application.Common.Models;
using Application.Services;

namespace Application.UnitTests
{
    public class SearchServiceTest
    {
        public IList<RateDto> example0 = new List<RateDto> {
            new RateDto { Date = new DateTime(2014,12,15),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.803818m},
                    {"GBP", 0.63935m},
                    {"JPY", 118.084399m},
                    {"RUB", 57.9957m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,16),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.799252m},
                    {"GBP", 0.634963m},
                    {"JPY", 116.831m},
                    {"RUB", 68.53245m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,17),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.810611m},
                    {"GBP", 0.642142m},
                    {"JPY", 118.468099m},
                    {"RUB", 68.30813m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,18),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.813927m},
                    {"GBP", 0.642142m},
                    {"JPY", 118.569499m},
                    {"RUB", 61.891725m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,19),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.817716m},
                    {"GBP", 0.639964m},
                    {"JPY", 119.3527m},
                    {"RUB", 58.9055m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,20),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.817703m},
                    {"GBP", 0.63999m},
                    {"JPY", 119.4555m},
                    {"RUB", 58.9055m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,21),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.818161m},
                    {"GBP", 0.63995m},
                    {"JPY", 119.5304m},
                    {"RUB", 58.67535m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,22),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.817742m},
                    {"GBP", 0.64158m},
                    {"JPY", 119.9486m},
                    {"RUB", 55.61375m}
                }
            },
            new RateDto { Date = new DateTime(2014,12,23),
                Rates = new Dictionary<string, decimal>
                {
                    {"EUR", 0.82153m},
                    {"GBP", 0.644409m},
                    {"JPY", 120.5218m},
                    {"RUB", 54.89676m}
                }
            }
        };

        public IList<RateDto> example1 = new List<RateDto> {
            new RateDto { Date = new DateTime(2014,12,15),
                Rates = new Dictionary<string, decimal> { {"RUB", 60.17m} }
            },
            new RateDto { Date = new DateTime(2014,12,16),
                Rates = new Dictionary<string, decimal> { {"RUB", 72.99m} }
            },
            new RateDto { Date = new DateTime(2014,12,17),
                Rates = new Dictionary<string, decimal> { {"RUB", 66.01m} }
            },
            new RateDto { Date = new DateTime(2014,12,18),
                Rates = new Dictionary<string, decimal> { {"RUB", 61.44m} }
            },
            new RateDto { Date = new DateTime(2014,12,19),
                Rates = new Dictionary<string, decimal> { {"RUB", 59.79m} }
            },
            new RateDto { Date = new DateTime(2014,12,20),
                Rates = new Dictionary<string, decimal> { {"RUB", 59.79m} }
            },
            new RateDto { Date = new DateTime(2014,12,21),
                Rates = new Dictionary<string, decimal> { {"RUB", 59.79m} }
            },
            new RateDto { Date = new DateTime(2014,12,22),
                Rates = new Dictionary<string, decimal> { {"RUB", 54.78m} }
            },
            new RateDto { Date = new DateTime(2014,12,23),
                Rates = new Dictionary<string, decimal> { {"RUB", 54.80m} }
            }
        };

        public IList<RateDto> example2 = new List<RateDto> {
            new RateDto { Date = new DateTime(2012,01,05),
                Rates = new Dictionary<string, decimal> { {"RUB", 40m} }
            },
            new RateDto { Date = new DateTime(2012,01,07),
                Rates = new Dictionary<string, decimal> { {"RUB", 35m} }
            },
            new RateDto { Date = new DateTime(2012,01,19),
                Rates = new Dictionary<string, decimal> { {"RUB", 30m} }
            },
        };

        public IList<RateDto> example3 = new List<RateDto> {
            new RateDto { Date = new DateTime(2012,01,05),
                Rates = new Dictionary<string, decimal> { {"RUB", 30m} }
            },
            new RateDto { Date = new DateTime(2012,01,07),
                Rates = new Dictionary<string, decimal> { {"RUB", 35m} }
            },
            new RateDto { Date = new DateTime(2012,01,19),
                Rates = new Dictionary<string, decimal> { {"RUB", 40m} }
            },
        };

        [Fact]
        public void SearchBestRevenue_GoodCondition1()
        {
            //arrange
            var searchService = new SearchService();
            var revenue = 27;
            var buyDate = new DateTime(2014, 12, 16);
            var sellDate = new DateTime(2014, 12, 22);
            var money = 100;
            var brokerFee = 1;

            //act
            var bestRevenue = searchService.SearchBestRevenue(example1, money, brokerFee);
            //assert
            Assert.NotNull(bestRevenue);
            Assert.Equal(revenue, (int)bestRevenue.Revenue);
            Assert.Equal(buyDate, bestRevenue.BuyDate);
            Assert.Equal(sellDate, bestRevenue.SellDate);
        }

        [Fact]
        public void SearchBestRevenue_GoodCondition2()
        {
            //arrange
            var searchService = new SearchService();
            var buyDate = new DateTime(2012, 01, 05);
            var sellDate = new DateTime(2012, 01, 07);
            var money = 50;
            var brokerFee = 1;

            //act
            var bestRevenue = searchService.SearchBestRevenue(example2, money, brokerFee);
            //assert
            Assert.NotNull(bestRevenue);
            Assert.Equal(buyDate, bestRevenue.BuyDate);
            Assert.Equal(sellDate, bestRevenue.SellDate);
        }

        [Fact]
        public void SearchBestRevenue_NotRevenue()
        {
            //arrange
            var searchService = new SearchService();
            var money = 50;
            var brokerFee = 1;

            //act
            var bestRevenue = searchService.SearchBestRevenue(example3, money, brokerFee);
            //assert
            Assert.NotNull(bestRevenue);
            Assert.Equal(0, (int)bestRevenue.Revenue);            
        }

        [Fact] public void SearchBestRevenue_RealCondition()
        {
            //arrange
            var searchService = new SearchService();
            var money = 100;
            var tool = "RUB";
            var brokerFee = 1;
            var buyDate = new DateTime(2014, 12, 17);
            var sellDate = new DateTime(2014, 12, 23);

            //act
            var bestRevenue = searchService.SearchBestRevenue(example0, money, brokerFee);
            //assert
            Assert.NotNull(bestRevenue);
            Assert.Equal(18, (int)bestRevenue.Revenue);
            Assert.Equal(buyDate, bestRevenue.BuyDate);
            Assert.Equal(sellDate, bestRevenue.SellDate);
            Assert.Equal(tool, bestRevenue.Tool);
        }
    }
}