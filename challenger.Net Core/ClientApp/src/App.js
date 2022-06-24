
import "bootstrap/dist/css/bootstrap.min.css"
import { useEffect, useState } from "react";

const App = () => {

    const [producto, setProductos] = useState([])
    const [categoria, setCategoria] = useState("");
    const [precio, setPrecio] = useState("");




    const mostrarProductos = async () => {

        const response = await fetch("api/producto/Lista");

        if (response.ok) {

            const data = await response.json();
            setProductos(data);
        } else {

            console.log("status code:" + response.status);
        }


    }

    //3.- Metodo convertir fecha
    const formatDate = (string) => {
        let options = { year: 'numeric', month: 'long', day: 'numeric' };
        let fecha = new Date(string).toLocaleDateString("es-PE", options);
        let hora = new Date(string).toLocaleTimeString();
        return fecha + " | " + hora
    }

    useEffect(() => {
        mostrarProductos();

    }, [])



    const guardarProducto = async (e) => {
        e.preventDefault()

        const response = await fetch("api/producto/Guardar", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ categoria: categoria })
        })

        if (response.ok) {
            setCategoria("");
            await mostrarProductos();

        }

        if (response.ok) {
            setPrecio("");
            await mostrarProductos();

        }
    }
    //    Para la venta se deberá contar con un filtro donde se ingrese el monto de dinero que dispone el cliente. La lista de productos se deberá filtrar en base a su precio
    const filtrarProductos = async (e) => {
        e.preventDefault()
        //Se deberá filtrar un único producto de cada categoría.
        const response = await fetch("api/producto/Filtrar", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ precio: precio })
        })

        if (response.ok) {
            const data = await response.json();
            setProductos(data);
        }
        else {
            console.log("status code:" + response.status);
        }
    }


    const cerrarTarea = async (id) => {

        const response = await fetch("api/producto/Cerrar/" + id, {
            method: "DELETE"
        })

        if (response.ok) {
            await mostrarProductos();
        }
    }




    return (
        <div className="container bg-dark p-4 vh-100">

            <h2 className="text-white">Lista de producto</h2>
            <div className="row">
                <div className="col-sm-12">
                    <form onSubmit={guardarProducto}>

                        <div className="input-group">
                            <input type="text"
                                className="form-control"
                                placeholder="Ingrese la descripcion de la producto"
                                value={categoria}
                                onChange={(e) => setCategoria(e.target.value)}
                            />
                            <button className="btn btn-success" type="submit">Agregar</button>
                        </div>
                    </form>

                </div>
            </div>

            <div className="row mt-4">
                <div className="col-sm-12">


                    <div className="list-group">
                        {
                            producto.map(
                                (item) => (
                                    <div key={item.idProducto} className="list-group-item list-group-item-action">
                                        <h5 className="text-primary">{item.categoria}</h5>
                                        <h5 className="text-secundary">{item.precio}</h5>


                                        <div className="d-flex justify-content-between">
                                            <small className="text-muted" >{formatDate(item.fechaCarga)}</small>
                                            <button onClick={() => cerrarTarea(item.idProducto)} className="btn btn-sm btn-outline-danger">Cerrar</button>
                                        </div>

                                    </div>

                                )

                            )


                        }

                    </div>
                    
                    <form onSubmit={filtrarProductos}>

                        <div className="input-group">
                            <input type="text"
                                className="form-control"
                                placeholder="Ingrese la preuspuesto del producto"
                                value={precio}
                                onChange={(e) => setPrecio(e.target.value)}
                            />
                            <button className="btn btn-success" type="submit">Presupuesto</button>
                        </div>
                    </form>


                </div>
            </div>
        </div>
    )
}


export default App;