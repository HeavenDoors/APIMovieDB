
$(document).ready(function () {
    getPeliculas()
})


function getPeliculas() {
    $.ajax({
        url: '/Peliculas/Get',
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            if (result.correct) {
                cards = $('#cards').empty();

                $.each(result.objects, function (index, pelicula) {

                    var urlImg = `https://image.tmdb.org/t/p/w500/${pelicula.backdrop_path}`
                    cards.append(`<div class="col-3 mt-3">
                                   <div class="card" style="width: 18rem;">
                                    <div id="imagenCard" style="background-image: url('${urlImg}'); background-size: 300px 200px ; display: grid; height: 200px">
                                        <i class="bi bi-heart-fill m-2" style="color:white;cursor: pointer" onclick="Add(${pelicula.id})"></i>
                                    </div>
                                      <div class="card-body">
                                        <h5 class="card-title">${pelicula.original_title}</h5>
                                        <p class="card-text">${pelicula.overview}</p>
                                      </div>
                                   </div>
                                   </div>`)
                })
            }
        }
    })
}


function Add(id) {
    $.ajax({
        url: `/Peliculas/AddFavorite?id=${id}`,
        type: 'POST',
        dataType: 'JSON',
        success: function (result) {
            if (result.correct) {
                Swal.fire({
                    icon: "success",
                    title: "Agregado con exito",
                    text: "Se ha Agregado Exitosamente esta pelicula a favoritos"
                });
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Hubo un error",
                    text: "No se pudo anadir esta pelicula"
                });
            }
        }
    })
}