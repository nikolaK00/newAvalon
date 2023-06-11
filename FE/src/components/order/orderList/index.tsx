import React, { FC, useState } from "react";

import { LoadingButton } from "@mui/lab";

import ErrorPage from "../../../pages/error";
import {
  useCancelOrderMutation,
  useGetOrdersQuery,
} from "../../../services/orderService";
import Table from "../../../shared/table";
import { useTablePagination } from "../../../shared/table/hooks";
import { Order, OrderResponse } from "../types";

import { orderColumns } from "./columns";

interface OrderListProps {
  newOrders?: boolean;
}

const OrderList: FC<OrderListProps> = ({ newOrders }) => {
  const [cancelOrderId, setCancelOrderId] = useState<string | null>();

  const { page, itemsPerPage, handleChangePage, handleChangeItemsPerPage } =
    useTablePagination();

  const { data, error, isLoading } = useGetOrdersQuery({
    page: page,
    itemsPerPage,
    newOrders,
  });

  const [cancelOrder, { isLoading: isCancelling }] = useCancelOrderMutation();

  const { data: orders, totalCount } = data || {};

  const tableProps = {
    title: "Orders",
    page,
    itemsPerPage,
    handleChangePage,
    handleChangeItemsPerPage,
    totalCount,
  };

  const CancelAction = (row: OrderResponse) => (
    <LoadingButton
      color={"error"}
      variant={"contained"}
      sx={{ mx: 1, fontSize: 12 }}
      loading={isCancelling && cancelOrderId === row.id}
      onClick={() => {
        setCancelOrderId(row.id);
        cancelOrder(row.id);
      }}
    >
      Cancel
    </LoadingButton>
  );

  let columns = orderColumns;
  if (newOrders) {
    columns = [
      {
        id: "actions",
        label: "",
        format: (row: OrderResponse) => CancelAction(row),
      },
      ...columns,
    ];
  }

  return (
    <ErrorPage error={!!error} isLoading={isLoading}>
      <Table columns={columns} data={orders} {...tableProps} />
    </ErrorPage>
  );
};

export default OrderList;
