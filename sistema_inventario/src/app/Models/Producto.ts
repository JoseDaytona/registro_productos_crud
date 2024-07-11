export interface Producto{
    idProducto : number;
    nombre:string,
    cantidad:number;
    precio:number;
    idEstado:number;
    estado?:string;
}