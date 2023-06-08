import { NavLink } from "react-router-dom";

import AddIcon from "@mui/icons-material/Add";
import { Fab } from "@mui/material";
import Box from "@mui/material/Box";

import ErrorPage from "../../../pages/error";
import { NEW_PRODUCT_ROUTE, PRODUCTS_ROUTE } from "../../../routes";
import { useGetProductsQuery } from "../../../services/productService";
import Table from "../../../shared/table";
import { useTablePagination } from "../../../shared/table/hooks";

import { productColumns } from "./columns";

const ProductList = () => {
  const { page, itemsPerPage, handleChangePage, handleChangeItemsPerPage } =
    useTablePagination();

  const { data, error, isLoading } = useGetProductsQuery({
    page: page,
    itemsPerPage,
    onlyActive: true,
  });

  const { data: products, totalCount } = data || {};

  const tableProps = {
    title: "Products",
    page,
    itemsPerPage,
    handleChangePage,
    handleChangeItemsPerPage,
    totalCount,
    navigateRoute: PRODUCTS_ROUTE,
  };

  return (
    <>
      <ErrorPage error={!!error} isLoading={isLoading}>
        <Table columns={productColumns} data={products} {...tableProps} />
      </ErrorPage>

      <Box sx={{ position: "absolute", bottom: 20, right: 20 }}>
        <NavLink to={NEW_PRODUCT_ROUTE}>
          <Fab color="primary" aria-label="add product" variant={"extended"}>
            <AddIcon sx={{ mr: 1 }} />
            Add Item
          </Fab>
        </NavLink>
      </Box>
    </>
  );
};

export default ProductList;
