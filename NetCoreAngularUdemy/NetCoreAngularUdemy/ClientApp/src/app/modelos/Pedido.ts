import { LineaPedido } from './LineaPedido';

export interface Pedido {
  email: string;
  DetallesPedido?: LineaPedido[];
}
