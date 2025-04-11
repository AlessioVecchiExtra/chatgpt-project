<template>

<div class="container-fluid">
    <div class="row">
      <header class="position-relative mt-3 w-100 text-center mb-5 col-12">
        <img src="../images/title.webp">
      </header>
      <h5 class="text-center mb-4">Seleziona una sessione per iniziare.</h5>
      <div v-if="questions" class="col-12 text-center d-flex justify-content-center gap-3">
        <router-link v-for="question in questions" :to="`/session/${question.id}`" class="btn btn-outline-primary btn-lg border-secondary">Sessione {{ question.id }}<i class="bi bi-arrow-right-short"></i></router-link>
        <!-- 
        <router-link to="/session/1" class="btn btn-outline-primary btn-lg border-secondary">Sessione 1 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/session/2" class="btn btn-outline-primary btn-lg border-secondary">Sessione 2 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/session/3" class="btn btn-outline-primary btn-lg border-secondary">Sessione 3 <i class="bi bi-arrow-right-short"></i></router-link> 
        -->
      </div>

      <div class="my-5"></div>
      <h5 class="text-center mb-4">Risultati</h5>
      <div class="col-12 text-center d-flex justify-content-center gap-3">
        <router-link to="/results?sessionIds=1" class="btn btn-outline-dark btn-lg">Sessione 1 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/results?sessionIds=2" class="btn btn-outline-dark btn-lg">Sessione 2 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/results?sessionIds=1,2" class="btn btn-outline-dark btn-lg">Sessione 1 + 2 <i class="bi bi-arrow-right-short"></i></router-link>
        <router-link to="/results?sessionIds=3" class="btn btn-outline-dark btn-lg">Sessione 3 <i class="bi bi-arrow-right-short"></i></router-link>        
        <router-link to="/results?sessionIds=1,2,3" class="btn btn-outline-dark btn-lg">Tutte le sessioni <i class="bi bi-arrow-right-short"></i></router-link>
      </div>

      <div class="my-5"></div>
      <h5 class="text-center mb-4">Reset Sessione</h5>
      <div class="col-12 text-center d-flex justify-content-center gap-3">
        
        <div v-if="questions" class="col-12 text-center d-flex justify-content-center gap-3">
            <button v-for="question in questions" @click="resetSession(question.id)" class="btn btn-outline-danger btn-lg border-secondary">Reset {{ question.id }}<i class="bi bi-arrow-right-short"></i></button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent, onMounted, computed, ref, watch } from 'vue'
import { useQuestionsStore  } from '@/store/questions'
import { useSessionsStore  } from '@/store/sessions'

export default defineComponent({
  name: 'HomeView',
  setup() {
    const questionStore = useQuestionsStore();
    const useSessionStore = useSessionsStore();

    const questions = ref([]);
    onMounted(async () => {
      questions.value = await questionStore.loadQuestions();
    });  

    const resetSession = async function(questionId){
      if(confirm(`sei sicuro di cancellare tutte le risposte della sessione ${questionId}?`)){      
        await useSessionStore.clearVotes(questionId);
      }
    }
    return {
      questions,
      resetSession
    }  
  }
});

</script>