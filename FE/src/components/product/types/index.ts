import { ListQueryParams } from "../../../services/types";
import { Entity } from "../../../shared/types";

export interface Product extends Entity {
  name: string;
  price: number;
  amount: number;
  description: string;
  image: string;
}

export interface ProductQueryParams extends ListQueryParams {
  onlyActive: boolean;
}

export interface ProductFormFields extends Omit<Product, "id"> {}
