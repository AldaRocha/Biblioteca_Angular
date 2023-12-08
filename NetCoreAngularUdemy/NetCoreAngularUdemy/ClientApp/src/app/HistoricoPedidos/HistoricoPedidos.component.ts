import { Component, OnInit/*, TemplateRef, ViewChild*/ } from '@angular/core';
import { AuthAPI } from '../modelos/authAPI';
import { UsuarioApiService } from '../services/usuarioApi.service';
import { environment } from '../../environments/environment';
import { Cliente } from '../modelos/cliente';
import { ProductoService } from '../services/producto.service';
import { PedidoDetalle } from '../modelos/PedidoDetalle';
//import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-HistoricoPedidos-component',
  templateUrl: './HistoricoPedidos.component.html'
})

export class HistoricoPedidosComponent implements OnInit {
  usuarioAPI: AuthAPI;
  cliente: Cliente;
  listaPedidos: PedidoDetalle[];
  detalleAux: number = 0;
  //@ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any>;

  constructor(private servicioLogin: UsuarioApiService, private servicioProducto: ProductoService/*, private modalService: NgbModal*/) {
    this.usuarioAPI = {
      email: environment.usuarioAPI,
      password: environment.passAPI
    }
  }

  ngOnInit(): void {
    if (sessionStorage.getItem('token') == null) {
      this.servicioLogin.loginAPI(this.usuarioAPI).subscribe(res => {
        if (res.error != null && res.error != '')
          console.log("Error al obtener el token");
        else
          this.damePedidos();
      })
    }
    else {
      this.damePedidos();
    }
  }

  damePedidos(): void {
    let usuarioSesion = JSON.parse(localStorage.getItem('emailLogin') || '{}');
    this.cliente = { email: usuarioSesion.email, rol: "" };
    this.servicioProducto.ObtenerPedidos(this.cliente).subscribe(res => {
      if (res.error != null && res.error != '') {
        console.log("Error al obtener los pedidos");
      }
      else {
        this.listaPedidos = res.objetoGenerico;

        console.log(this.listaPedidos);
      }
    })
  }

  detalles(indice: number) {
    this.detalleAux = indice;
    console.log(this.listaPedidos[indice].detallesProductosPedido[0].nombreProducto);
    //this.modalService.open(this.myModalInfo);
  }
}
