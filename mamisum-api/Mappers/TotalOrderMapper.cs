using mamisum_api.DTOs;
using mamisum_api.Models;

namespace mamisum_api.Mappers
{
    public class TotalOrderMapper
    {
        public static TotalOrder ToModel(CreateTotalOrderDto dto)
        {
            return new TotalOrder
            {
                OrderNo = dto.OrderNo,
                TotalBillAmount = dto.TotalBillAmount,
                DeliveryStatus = dto.DeliveryStatus
            };
        }

        public static void MapUpdate(TotalOrder order, UpdateTotalOrderDto dto)
        {
            order.TotalBillAmount = dto.TotalBillAmount;
            order.DeliveryStatus = dto.DeliveryStatus;
        }
    }
}
