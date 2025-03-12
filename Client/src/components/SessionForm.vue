<template>
  <div>
    <p>Scegli la parola che rappresenta meglio questa sessione:</p>
    <div>
      <button
        v-for="wordOption in wordOptions"
        :key="wordOption"
        @click="selectWord(wordOption)"
      >
        {{ wordOption }}
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
    }
  },
  setup(props) {
    const sessionsStore = useSessionsStore()

    async function selectWord(word) {
      await sessionsStore.addWordToSession(props.sessionId, word)
    }

    return {
      selectWord,
      wordOptions: [
        'Innovazione', 'Collaborazione', 'Creatività', 'Ispirazione', 'Flessibilità',
        'Cambiamento', 'Coinvolgimento', 'Efficienza', 'Qualità', 'Eccellenza'
      ]
    }
  }
})
</script>
