import ErrorPage from "../../../pages/error";
import { useGetOrdersQuery } from "../../../services/orderService";
import Table from "../../../shared/table";
import { useTablePagination } from "../../../shared/table/hooks";

import { orderColumns } from "./columns";

const OrderList = () => {
  const { page, itemsPerPage, handleChangePage, handleChangeItemsPerPage } =
    useTablePagination();

  const { data, error, isLoading } = useGetOrdersQuery({
    page: page,
    itemsPerPage,
  });

  const { data: orders, totalCount } = data || {};

  const tableProps = {
    title: "Orders",
    page,
    itemsPerPage,
    handleChangePage,
    handleChangeItemsPerPage,
    totalCount,
  };

  return (
    <ErrorPage error={!!error} isLoading={isLoading}>
      <Table columns={orderColumns} data={orders} {...tableProps} />
    </ErrorPage>
  );
};

export default OrderList;
