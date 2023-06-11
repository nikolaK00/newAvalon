import React, { FC, useEffect, useState } from "react";
import { useSelector } from "react-redux";

import { LoadingButton } from "@mui/lab";

import ErrorPage from "../../../pages/error";
import {
  useCancelOrderMutation,
  useGetOrdersQuery,
} from "../../../services/orderService";
import Table from "../../../shared/table";
import { useTablePagination } from "../../../shared/table/hooks";
import { RootState } from "../../../store";
import { Role } from "../../user/types";
import { OrderResponse } from "../types";

import { orderColumns } from "./columns";

interface OrderListProps {
  newOrders?: boolean;
}

const OrderList: FC<OrderListProps> = ({ newOrders = false }) => {
  const [cancelOrderId, setCancelOrderId] = useState<string | null>();

  const { roles } = useSelector((state: RootState) => state.user);

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

  let columns = orderColumns(newOrders);
  if (newOrders && roles === Role.customer) {
    columns = [
      {
        id: "actions",
        label: "",
        format: (row: OrderResponse) => CancelAction(row),
      },
      ...columns,
    ];
  }

  useEffect(() => {
    return () => {
      handleChangePage(undefined, 1);
    };
  }, [newOrders]);

  return (
    <ErrorPage error={!!error} isLoading={isLoading}>
      <Table columns={columns} data={orders} {...tableProps} />
    </ErrorPage>
  );
};

export default OrderList;
