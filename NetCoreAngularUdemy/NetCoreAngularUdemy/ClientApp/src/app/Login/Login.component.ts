import { Component, OnInit/*, TemplateRef, ViewChild*/ } from '@angular/core'
//import { ClienteService } from '../services/cliente.service';
import { environment } from '../../environments/environment';
import { AuthAPI } from '../modelos/authAPI';
import { UsuarioApiService } from '../services/usuarioApi.service';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UsuarioAPI } from '../modelos/usuarioAPI';
import { Cliente } from '../modelos/cliente';
import { ClienteService } from '../services/cliente.service';
import { Router } from '@angular/router';
//import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-Login-component',
  templateUrl: './Login.component.html'
})

export class LoginComponent implements OnInit {
  usuarioAPI: AuthAPI;
  loginForm!: FormGroup;
  enviado = false;
  resultadoPeticion: string = '';
  //@ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any>;
  //token: string = '';

  constructor(
    /*private servicioProducto: ClienteService,*/
    private servicioLogin: UsuarioApiService,
    private formBuilder: FormBuilder
    /*, private modalService: NgbModal*/,
    private servicioCliente: ClienteService,
    private router: Router
  ) {
    //servicioProducto.dameclientes().subscribe(res => { console.log(res) });
    this.usuarioAPI = {
      email: environment.usuarioAPI,
      password: environment.passAPI
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      pass: ['', Validators.required]
    })
    // Login API
    if (sessionStorage.getItem('token') == null) {
      this.servicioLogin.loginAPI(this.usuarioAPI).subscribe(res => {
        if (res.error != null && res.error != '')
          this.resultadoPeticion = res.texto;
        //else
        //  this.token = this.servicioLogin.tokenAPI;
      })
    }
    //else {
    //  this.token = this.servicioLogin.tokenAPI;
    //}
  }

  get f(): { [key: string]: AbstractControl } {
    return this.loginForm.controls;
  }

  Login() {
    this.enviado = true;
    if (this.loginForm.invalid) {
      console.log("Invalido");
      return;
    }
    let cliente: Cliente = {
      email: this.loginForm.controls['email'].value,
      pass: this.loginForm.controls['pass'].value,
      rol: ""
    }

    this.servicioCliente.loginCliente(cliente/*, this.token*/).subscribe(res => {
      if (res.error != null && res.error != '') {
        this.resultadoPeticion = res.texto;
        console.log(this.resultadoPeticion);
        //this.modalService.open(this.myModalInfo);
      }
      else {
        //this.resultadoPeticion = "Login correcto";
        console.log(res);
        this.router.navigate(['/Productos']);
      }
    })

    // Llamar método de Login incrustando el token en la cabecera
    //this.resultadoPeticion = this.token;
  }
}
