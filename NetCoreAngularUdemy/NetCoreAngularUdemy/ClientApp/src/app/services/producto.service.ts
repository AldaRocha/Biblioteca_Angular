import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Resultado } from '../modelos/resultado';
import { Resultado_Uni } from '../modelos/resultado_uni';
import { Pedido } from '../modelos/Pedido';
import { Cliente } from '../modelos/cliente';
import { Producto } from '../modelos/Producto';

@Injectable({
  providedIn: 'root'
})

export class ProductoService {
  url: string = 'https://localhost:7193/API/productos/';
  url_diana: string = 'http://192.168.221.150:8080/biblioteca/api/libro/getLibrosByTittle';
  url_gabo: string = 'http://192.168.221.234:8080/Libreria/api/libro/getAll'
  constructor(private peticion: HttpClient) {

  }

  agregarProductos(producto: Producto): Observable<Resultado> {
    return this.peticion.post<Resultado>(this.url + 'agregar', producto);
  }

  modificarProducto(producto: Producto, id: number): Observable<Resultado> {
    return this.peticion.put<Resultado>(this.url + 'actualizar/' + id, producto);
  }

  dameProductos(/*token: string*/): Observable<Resultado> {
    //var reqHeader = new HttpHeaders({
    //  'Content-Type': 'application/json',
    //  'Authorization': 'Bearer ' + token
    //});
    return this.peticion.get<Resultado>(this.url/*, { headers: reqHeader }*/);
  }

  dameProductosDiana(): Observable<Resultado_Uni> {
    return this.peticion.get<Resultado_Uni>(this.url_diana);
  }

  dameProductosGabo(): Observable<Resultado_Uni> {
    return this.peticion.get<Resultado_Uni>(this.url_gabo);
  }

  AgregarPedido(pedido: Pedido): Observable<Resultado> {
    return this.peticion.post<Resultado>(this.url, pedido);
  }

  ObtenerPedidos(cliente: Cliente): Observable<Resultado> {
    return this.peticion.post<Resultado>(this.url + "Pedidos", cliente);
  }
}
