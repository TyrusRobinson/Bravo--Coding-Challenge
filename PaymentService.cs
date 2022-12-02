using Sabio.Data.Providers;
using Sabio.Models.Domain.Friends;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Data;

namespace Sabio.Services
{
    public class PaymentService : IPaymentService
    {
        IDataProvider _data = null;
        public PaymentService(IDataProvider data)
        {
            _data = data;
        }

        public Payment Get(int userId)
        {
            string procName = "[dbo].[Payment_SelectById]";

            Payment payment = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramcollection)
            {


                paramcollection.AddWithValue("@UserId", userId);

            }, delegate (IDataReader reader, short set) //single Record Mapper
            {

                int startingIndex = 0;
                payment = MapPayment(reader, ref startingIndex);

            }
            );

            return payment;
        }

        private static Payment MapPayment(IDataReader reader, ref int startingIndex)
        {
            Payment aPayment = new Payment();


            aPayment.Id = reader.GetSafeInt32(startingIndex++);
            aPayment.UserId = reader.GetSafeInt32(startingIndex++);

            return aPayment;

        }
    }
}
