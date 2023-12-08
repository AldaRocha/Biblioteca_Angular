import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Resultado } from '../modelos/resultado';
import { Cliente } from '../modelos/cliente';

@Injectable({
  providedIn: 'root'
})

export class ClienteService {
  url: string = 'https://localhost:7193/API/clientes/';
  private emailLoginSubject: BehaviorSubject<Cliente>;
  public cliente: Observable<Cliente>;

  public get usuarioLogin(): Cliente {
    return this.emailLoginSubject.value;
  }

  constructor(private peticion: HttpClient) {
    this.emailLoginSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('emailLogin') || '{}'));
    this.cliente = this.emailLoginSubject.asObservable();
  }

  dameclientes(): Observable<Resultado> {
    return this.peticion.get<Resultado>(this.url);
  }

  agregarCliente(cliente: Cliente): Observable<Resultado> {
    return this.peticion.post<Resultado>(this.url + "AgregarClienteUniversidad", cliente);
  }

  modificarCliente(cliente: Cliente): Observable<Resultado> {
    return this.peticion.put<Resultado>(this.url, cliente);
  }

  bajaCliente(email: string): Observable<Resultado> {
    return this.peticion.delete<Resultado>(this.url + email);
  }

  loginCliente(cliente: Cliente/*, token: string*/): Observable<Resultado> {
    //var reqHeader = new HttpHeaders({
    //  'Content-Type': 'application/json',
    //  'Authorization': 'Bearer ' + token
    //});

    return this.peticion.post<Resultado>(this.url + "Login/Universidad", cliente/*, { headers: reqHeader }*/).pipe(map(result => {
      if (result.error == null || result.error == '') {
        const cliente: Cliente = (result.objetoGenerico as Cliente);
        localStorage.setItem('emailLogin', JSON.stringify(cliente));
        this.emailLoginSubject.next(cliente);
      }
      return result;
    }));
  }

  cierreSesion() {
    localStorage.removeItem('emailLogin');
    sessionStorage.removeItem('token');
    this.emailLoginSubject.next(null!);
  }

  damecliente(cliente: Cliente): Observable<Resultado> {
    return this.peticion.post<Resultado>(this.url + "Cliente", cliente);
  }
}
