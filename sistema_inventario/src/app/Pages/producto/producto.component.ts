import { Component, Input, OnInit, inject } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ProductoService } from '../../Services/producto.service';
import { Router } from '@angular/router';
import { Producto } from '../../Models/Producto';


@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.css'
})
export class ProductoComponent implements OnInit {

  @Input('id') idProducto!: number;
  private productoServicio = inject(ProductoService);
  public formBuild = inject(FormBuilder);

  public formProducto: FormGroup = this.formBuild.group({
    nombre: [''],
    cantidad: [0],
    precio: [0],
    idEstado: [0]
  });

  constructor(private router: Router) { }

  ngOnInit(): void {
    
    if (this.idProducto != 0) {
      this.productoServicio.obtener(this.idProducto).subscribe({
        next: (data) => {
          this.formProducto.patchValue({
            nombre: data.nombre,
            cantidad: data.cantidad,
            precio: data.precio
          })
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }
  }

  guardar() {
    const objeto: Producto = {
      idProducto: this.idProducto,
      nombre: this.formProducto.value.nombre,
      cantidad: this.formProducto.value.cantidad,
      precio: this.formProducto.value.precio,
      idEstado: this.formProducto.value.idEstado,
    }

    if (this.idProducto == 0) {
      this.productoServicio.crear(objeto).subscribe({
        next: (data) => {
          this.router.navigate(["/"]);
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    } else {
      this.productoServicio.editar(objeto).subscribe({
        next: (data) => {
          this.router.navigate(["/"]);
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }


  }

  volver() {
    this.router.navigate(["/"]);
  }

}
