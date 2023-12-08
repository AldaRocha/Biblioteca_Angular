import { Component, OnInit } from "@angular/core";
import { ProductoService } from "../services/producto.service";
import { FormBuilder, FormGroup } from "@angular/forms";
import { Producto } from "../modelos/Producto";

@Component({
  selector: 'app-administrar-component',
  templateUrl: './administrarlibros.component.html'
})

export class AdministrarLibros implements OnInit{
  librosForm!: FormGroup;
  public listaProductos!: any[];
  cadenaPdf: string = "";

  constructor(private libroServices: ProductoService, private formBuilder: FormBuilder) {
    
  }

  ngOnInit(): void{
    this.librosForm = this.formBuilder.group({
      idLibro: [''],
      nombre: [''],
      precio: [''],
      descripcion: [''],
      pdf: ['']
    });
    this.dameProductos();
  }

  dameProductos() {
    this.libroServices.dameProductos().subscribe(res => {
      if (res.error) {
        alert(res.texto);
      } else {
        this.listaProductos = res.objetoGenerico;
      }
    })
  }

  Enviar() {
    const producto: Producto = {
      nombre: this.librosForm.controls['nombre'].value,
      precio: this.librosForm.controls['precio'].value,
      descripcion: this.librosForm.controls['descripcion'].value,
      pdf: this.cadenaPdf
    };

    const id: number = this.librosForm.controls['idLibro'].value;

    if (id > 0) {
      this.libroServices.modificarProducto(producto, id).subscribe(res => {
        if (res.error) {
          alert(res.texto);
        } else {
          alert("Libro actualizado con exito");
        }
      });
    } else {
      this.libroServices.agregarProductos(producto).subscribe(res => {
        if (res.error) {
          alert(res.texto);
        } else {
          alert("Libro registrado con exito.");
        }
      });
    }
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    const reader = new FileReader();

    reader.onload = (e: any) => {
      const base64Data: string = e.target.result;
      console.log('Archivo en base64:', base64Data);
      this.cadenaPdf = base64Data;
    };

    if (file) {
      reader.readAsDataURL(file);
    }
  }

}
