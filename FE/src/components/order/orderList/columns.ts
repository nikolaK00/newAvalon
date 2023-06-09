import { OrderResponse, OrderStatus } from "../types";

export const orderColumns = [
  {
    id: "name",
    label: "Name",
  },
  {
    id: "product",
    label: "Product",
  },
  {
    id: "dealer",
    label: "Dealer",
    format: (row: OrderResponse) =>
      `${row.dealerId.firstName} ${row.dealerId.lastName}`,
  },
  {
    id: "totalPrice",
    label: "Total Price",
    format: (row: OrderResponse) => `$${row.fullPrice}, 00`,
  },
  {
    id: "status",
    label: "Status",
    format: (row: OrderResponse) => OrderStatus[row.status],
  },
];
