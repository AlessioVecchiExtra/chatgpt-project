<template>
      <div v-for="wordOption in terms"
      :key="wordOption.Id"
      class="col">
      <button
        class="btn btn-outline-primary border-secondary btn-lg w-100"
        @click="selectWord(wordOption)"
      >
        {{ wordOption.answerText }}
      </button>   
    </div>
</template>

<script>
import { useSessionsStore } from '@/store/sessions'
import { defineComponent } from 'vue'
import { useRouter } from 'vue-router'

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
  emits: ["voted"],
  setup(props, { emit }) {
    const sessionsStore = useSessionsStore()
    const router = useRouter();

    async function selectWord(word) {

      emit('voted', word.id, word.answerText);
      // await sessionsStore.addWordToSession(props.sessionId, word.id, word.answerText)     
      // router.push({ path: `/thankyou`})
      // //router.push({ path: `/thankyou/${props.sessionId}`})
    }

    return {
      selectWord
    }
  }
})
</script>
