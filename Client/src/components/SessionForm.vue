<template>
  <div class="container">
    <p class="lead">Scegli la parola che rappresenta meglio questa sessione:</p>
    <div class="d-flex flex-wrap gap-2">
      <button
        v-for="wordOption in terms"
        :key="wordOption.Id"
        class="btn btn-outline-primary"
        @click="selectWord(wordOption)"
      >
        {{ wordOption.answerText }} - {{ wordOption.id }}
      </button>   
    </div>
  </div>
</template>

<script>
import { useSessionsStore } from '@/store/sessions'
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'SessionForm',
  props: {
    sessionId: {
      type: String,
      required: true
    },
    terms: {
      type: Array,
      default: () => []
    }
  },
  setup(props) {
    const sessionsStore = useSessionsStore()

    async function selectWord(word) {
      await sessionsStore.addWordToSession(props.sessionId, word.id, word.answerText)
    }

    return {
      selectWord
    }
  }
})
</script>
