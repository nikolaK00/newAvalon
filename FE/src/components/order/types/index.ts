import { Entity } from "../../../shared/types";

export enum OrderStatus {}

export interface Order extends Entity {
  name: string;
  comment: string;
  deliveryAddress: string;
  status: OrderStatus;
  deliveryPrice: number;
  fullPrice: number;
  deliveryOnUtc: Date;
}

export interface OrderFormFields extends Omit<Order, "id"> {}
