import dayjs from "dayjs";

import { OrderResponse, OrderStatus } from "../types";

export const orderColumns = (areNewOrders: boolean) => [
  {
    id: "name",
    label: "Name",
  },
  {
    id: "product",
    label: "Product",
    format: (row: OrderResponse) => <>{row.productDetailsResponses[0].name}</>,
  },
  {
    id: "dealer",
    label: "Dealer",
    format: (row: OrderResponse) => (
      <>
        {row.dealerId.firstName} {row.dealerId.lastName}
      </>
    ),
  },
  {
    id: "delivery",
    label: areNewOrders ? "Delivery on" : "Delivered on",
    format: (row: OrderResponse) => (
      <>{dayjs(row.deliveryOnUtc).format("DD/MM/YYYY HH:mm")}</>
    ),
  },
  {
    id: "totalPrice",
    label: "Total Price",
    format: (row: OrderResponse) => <>${row.fullPrice}, 00</>,
  },
  {
    id: "status",
    label: "Status",
    format: (row: OrderResponse) => <>{OrderStatus[row.status]}</>,
  },
];
