import { Component } from '@angular/core';
import { Cliente } from '../modelos/cliente';
import { ClienteService } from '../services/cliente.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  cliente?: Cliente;
  logado?: boolean;
  admin: boolean = false;

  constructor(public servicioCliente: ClienteService, private router: Router) {
    this.servicioCliente.cliente.subscribe(res => {
      this.cliente = res;
      this.logado = (this.cliente == null || typeof this.cliente.email == "undefined") ? false : true;
    })
    this.esAdmin();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  cierreSesion() {
    this.servicioCliente.cierreSesion();
    this.router.navigate(['/login']);
    this.logado = false;
  }

  esAdmin() {
    let usuarioSesion = JSON.parse(localStorage.getItem('emailLogin') || '{}');
    this.admin = (usuarioSesion.rol == "Administrador") ? true : false;
  }
}
