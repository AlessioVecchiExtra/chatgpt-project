<template>
    <div class="row justify-content-center row-cols-2 row-cols-lg-auto g-lg-4 g-3 row-gap-4">
      <div v-for="wordOption in terms"
      :key="wordOption.Id"
      class="col">
      <button
        class="btn btn-outline-primary border-secondary btn-lg w-100 fs-6"
        @click="selectWord(wordOption)"
      >
        {{ wordOption.answerText }}
      </button>   
    </div>
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
  setup(props) {
    const sessionsStore = useSessionsStore()
    const router = useRouter();

    async function selectWord(word) {
      await sessionsStore.addWordToSession(props.sessionId, word.id, word.answerText)     
      router.push({ path: `/thankyou`})
      //router.push({ path: `/thankyou/${props.sessionId}`})
    }

    return {
      selectWord
    }
  }
})
</script>
