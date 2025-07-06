import { StockType } from "./stock-type.enum";


export interface Stock {
  id?: number;
  name: string;
  description: string;
  stockType: StockType;
  quantity: number;
  isDozen: boolean;
  }