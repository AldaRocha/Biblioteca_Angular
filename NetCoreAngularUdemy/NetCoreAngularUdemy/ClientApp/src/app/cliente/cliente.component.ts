import { Component, Input, OnInit/*, TemplateRef, ViewChild*/ } from '@angular/core'
import { Cliente } from '../modelos/cliente';
import { ClienteService } from '../services/cliente.service';
import { FormBuilder, FormGroup, Validator, AbstractControl, Validators } from '@angular/forms';
import { AuthAPI } from '../modelos/authAPI';
import { environment } from '../../environments/environment';
import { UsuarioApiService } from '../services/usuarioApi.service';
//import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-cliente-component',
  templateUrl: './cliente.component.html'
})

export class ClienteComponent implements OnInit {
  altaForm!: FormGroup;
  enviado = false;
  resultadoPeticion: string = "";
  admin: boolean = false;
  //@ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any>;

  //@Input() nombre: string = "";
  //@Input() email: string = "";

  //nombreQueryString: string = "";
  //emailQueryString: string = "";
  usuarioAPI: AuthAPI;

  constructor(/*private route: ActivatedRoute,*/ private servicioCliente: ClienteService, private formBuilder: FormBuilder/*, private modalService: NgbModal*/, private servicioLogin: UsuarioApiService) {
    //this.route.queryParams.subscribe(params => {
    //  this.nombreQueryString = params['nombre'];
    //  this.emailQueryString = params['email'];
    //})
    this.usuarioAPI = {
      email: environment.usuarioAPI,
      password: environment.passAPI
    }
    this.esAdmin();
  }

  ngOnInit(): void {
    this.altaForm = this.formBuilder.group({
      nombre: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      pass: ['', Validators.required],
      rol: ['', Validators.required]
    })

    if (sessionStorage.getItem('token') == null) {
      this.servicioLogin.loginAPI(this.usuarioAPI).subscribe(res => {
        if (res.error != null && res.error != '')
          console.log("Error al obtener el token");
      })
    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.altaForm.controls;
  }

  public Alta() {
    this.enviado = true;
    if (this.altaForm.invalid) {
      console.log("Invalido");
      return;
    }
    console.log("valido");
    const cliente: Cliente = {
      nombre: this.altaForm.controls['nombre'].value,
      email: this.altaForm.controls['email'].value,
      pass: this.altaForm.controls['pass'].value,
      rol: this.altaForm.controls['rol'].value,
      estatus: 1
    };
    this.servicioCliente.agregarCliente(cliente).subscribe(res => {
      if (res.error != null && res.error != '')
        this.resultadoPeticion = res.texto;
      else {
        this.resultadoPeticion = "Cliente dado de alta corrctamente. Inicie sesión."
      }
      //this.modalService.open(this.myModalInfo);
      console.log(this.resultadoPeticion);
      alert(this.resultadoPeticion);
    });
    //this.servicioCliente.modificarCliente(cliente).subscribe(res => {
    //  if (res.error != null && res.error != '')
    //    this.resultadoPeticion = res.texto;
    //  else {
    //    this.resultadoPeticion = "Cliente actualizado de forma correcta. Inicie sesión."
    //  }
    //  //this.modalService.open(this.myModalInfo);
    //  console.log(this.resultadoPeticion);
    //  alert(this.resultadoPeticion);
    //});
    //this.servicioCliente.bajaCliente(this.altaForm.controls['email'].value).subscribe(res => {
    //  if (res.error != null && res.error != '')
    //    this.resultadoPeticion = res.texto;
    //  else {
    //    this.resultadoPeticion = "Eliminado"
    //  }
    //  //this.modalService.open(this.myModalInfo);
    //  console.log(this.resultadoPeticion);
    //  alert(this.resultadoPeticion);
    //});
  }

  esAdmin() {
    let usuarioSesion = JSON.parse(localStorage.getItem('emailLogin') || '{}');
    this.admin = (usuarioSesion.rol == "Administrador") ? true : false;
  }
}
