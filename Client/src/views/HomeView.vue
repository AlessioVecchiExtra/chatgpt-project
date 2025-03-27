<template>
  <div class="container">
    <h1>Benvenuti all'evento</h1>
    <div class="my-2">
      <p>Seleziona una sessione per iniziare.</p>
      <div class="d-flex flex-row">
        <router-link to="/session/1" class="btn btn-outline-primary me-2">Sessione 1 &ge;</router-link>
        <router-link to="/session/2" class="btn btn-outline-primary me-2">Sessione 2 &ge;</router-link>
        <router-link to="/session/3" class="btn btn-outline-primary me-2">Sessione 3 &ge;</router-link>
      </div>
    </div>
    <div class="my-5">
      <p>Risultati</p>
      <div class="d-flex flex-column">
        <router-link to="/results/1" class="me-2">Vedi risultati Sessione 1 &ge;</router-link>
        <router-link to="/results/2" class="me-2">Vedi risultati Sessione 2 &ge;</router-link>
        <router-link to="/results/3" class="me-2">Vedi risultati Sessione 3 &ge;</router-link>
      </div>
    </div>
  </div>
</template>
