import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ProductoService } from '../../Services/producto.service';
import { Producto } from '../../Models/Producto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatIconModule, MatButtonModule],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {

  private productoServicio = inject(ProductoService);
  public listaProducto: Producto[] = [];
  public displayedColumns: string[] = ['nombre', 'cantidad', 'precio', 'estado', 'accion'];

  obtenerProductos() {
    this.productoServicio.lista().subscribe({
      next: (data) => {
        if (data.length > 0) {
          this.listaProducto = data;
        }
      },
      error: (err) => {
        console.log(err.message)
      }
    })
  }

  constructor(private router: Router) {

    this.obtenerProductos();
  }

  nuevo() {
    this.router.navigate(['/producto', 0]);
  }

  editar(objeto: Producto) {
    this.router.navigate(['/producto', objeto.idProducto]);
  }
  eliminar(objeto: Producto) {
    if (confirm("Desea eliminar el producto " + objeto.nombre)) {
      this.productoServicio.eliminar(objeto.idProducto).subscribe({
        next: (data) => {
          //if (data.Respuesta) {
            this.obtenerProductos();
          // } else {
          //   alert("no se pudo eliminar")
          // }
        },
        error: (err) => {
          console.log(err.message)
        }
      })
    }
  }
}
