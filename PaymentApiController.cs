using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Services.Interfaces;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Models.Domain.Friends;
using Sabio.Web.Models.Responses;
using System.Data.SqlClient;
using System;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentApiController : BaseApiController
    {
        private IPaymentService _service = null;
        private IAuthenticationService<int> _authService = null;
        public PaymentApiController(IPaymentService service
            , ILogger<PaymentApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }


        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Payment>> Get(int userId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {

                Payment payment = _service.Get(userId);

                if (payment == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");

                }
                else
                {
                    response = new ItemResponse<Payment> { Item = payment };

                }
            }
            catch (SqlException sqlEx)
            {
                iCode = 500;

                response = new ErrorResponse($"SqlException Error: {sqlEx.Message}");

                base.Logger.LogError(sqlEx.ToString());



            }
            catch (ArgumentException argEx)
            {
                iCode = 500;
                response = new ErrorResponse($"ArgumentException Error: {argEx.Message}");


            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");

            }

            return StatusCode(iCode, response);

        }
    }
}
