import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
//import { HomeComponent } from './home/home.component';
//import { CounterComponent } from './counter/counter.component';
//import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { InicioComponent } from './inicio/inicio.component';
import { ClienteComponent } from './cliente/cliente.component';
import { LoginComponent } from './Login/Login.component';
import { ProductoComponent } from './Productos/Productos.component';
import { AutenticacionGuard } from './Seguridad/autenticacion.guard';
import { TokenInterceptor } from './Seguridad/token.interceptor';
import { HistoricoPedidosComponent } from './HistoricoPedidos/HistoricoPedidos.component';
import { MisDatosComponent } from './MisDatos/misdatos.component';
import { AdministrarLibros } from './administrar/administrarlibros.component';
//import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    //HomeComponent,
    //CounterComponent,
    //FetchDataComponent,
    InicioComponent,
    ClienteComponent,
    LoginComponent,
    ProductoComponent,
    HistoricoPedidosComponent,
    MisDatosComponent,
    AdministrarLibros
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    //NgbModule,
    RouterModule.forRoot([
      { path: '', component: /*HomeComponent*/InicioComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent },
      { path: 'inicio', component: InicioComponent },
      { path: 'cliente', component: ClienteComponent },
      { path: 'login', component: LoginComponent },
      { path: 'Productos', component: ProductoComponent, canActivate: [AutenticacionGuard] },
      { path: 'historicoPedidos', component: HistoricoPedidosComponent, canActivate: [AutenticacionGuard] },
      { path: 'MisDatos', component: MisDatosComponent, canActivate: [AutenticacionGuard] },
      { path: 'administrar', component: AdministrarLibros, canActivate: [AutenticacionGuard] }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
