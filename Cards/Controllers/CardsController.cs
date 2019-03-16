using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cards.DBContext;
using Cards.Helper;
using Cards.IntegrationEvents;
using Cards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Saga;

namespace Cards.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private Cards.Helper.ResultEntity _res;
        private readonly ReturnResultDataHelper _resHelper;
        private readonly RIOCardsContext _cardsContext;
        private readonly ICardsIntegrationEventService _cardsIntegrationEventService;
        private readonly CardsSettings _settings;

        public CardsController(RIOCardsContext context, IOptionsSnapshot<CardsSettings> settings, ICardsIntegrationEventService cardsIntegrationEventService)
        {
            _cardsContext = context ?? throw new ArgumentNullException(nameof(context));
            _cardsIntegrationEventService = cardsIntegrationEventService ?? throw new ArgumentNullException(nameof(cardsIntegrationEventService));
            _resHelper = new ReturnResultDataHelper();
            _settings = settings.Value;
        }

        // public CardsController(RIOCardsContext context, ICardsIntegrationEventService cardsIntegrationEventService)


        // GET: api/<controller>
        [HttpPost]
        [Route("api/Card")]
        //  public async Task<IEnumerable<GrandParentModel>> GetUsers()
        public async Task<IActionResult> CreateCard([FromBody] Card obj )
        {
            try
            {
                var item = new Cards.DBContext.Cards
                {
                    CardId = obj.Username,
                    CorrelationId = obj.Correlationid,
                    CompanyName = obj.CompanyName,
                    CardHolderName = obj.CardHolderName,
                    TransactionStatus =(int) TransactionStatusEnum.Inserted
                };

                _cardsContext.Cards.Add(item);
                //raise event for card creation
                var cardorderrequestEvent = new CardOrderRequestIntegrationEvent(obj.Username, obj.Correlationid, obj.CompanyName, obj.CardHolderName);

                // Achieving atomicity between original Cards database operation and the IntegrationEventLog thanks to a local transaction
                   await _cardsIntegrationEventService.SaveEventAndCardsContextChangesAsync(cardorderrequestEvent);

                // Publish through the Event Bus and mark the saved event as published
                 await _cardsIntegrationEventService.PublishThroughEventBusAsync(cardorderrequestEvent);

                //if (grandParentDetails.Count != 0)
                //    return Ok(grandParentDetails);
                return Ok();
                //_res = _resHelper.GetFailureResultEntity("Failure", 601, "No Parents Configured");
                //return new BadRequestObjectResult(_res);

            }
            catch (Exception ex)
            {

                _res = _resHelper.GetFailureResultEntity("Failure", 701, ex.Message);
                return new BadRequestObjectResult(_res);
            }
        }
    }
}
