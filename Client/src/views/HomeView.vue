<template>

<div class="container-fluid">
    <div class="row">
      <header class="position-relative mt-3 w-100 text-center mb-5 col-12">
        <img src="../images/title.webp">
      </header>
      <h5 class="text-center mb-4">Seleziona una sessione per iniziare.</h5>
      <div class="col-12 text-center d-flex justify-content-center gap-3">
        <router-link to="/session/1" class="btn btn-outline-primary btn-lg border-secondary">Sessione 1 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/session/2" class="btn btn-outline-primary btn-lg border-secondary">Sessione 2 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/session/3" class="btn btn-outline-primary btn-lg border-secondary">Sessione 3 <i class="bi bi-arrow-right-short"></i></router-link>
      </div>

      <div class="my-5"></div>
      <h5 class="text-center mb-4">Risultati</h5>
      <div class="col-12 text-center d-flex justify-content-center gap-3">
        <router-link to="/results?sessionIds=1" class="btn btn-outline-dark btn-lg">Sessione 1 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/results?sessionIds=2" class="btn btn-outline-dark btn-lg">Sessione 2 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/results?sessionIds=1,2" class="btn btn-outline-dark btn-lg">Sessione 1 + 2 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/results?sessionIds=3" class="btn btn-outline-dark btn-lg">Sessione 3 <i class="bi bi-arrow-right-short"></i></router-link>        
        <router-link to="/results/sessionIds=1,2,3" class="btn btn-outline-dark btn-lg">Tutte le sessioni <i class="bi bi-arrow-right-short"></i></router-link>
      </div>

    </div>
  </div>
</template>
