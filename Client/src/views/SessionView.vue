<template>
  <div>
    <h1>Sessione {{ sessionId }}</h1>
    <SessionForm :sessionId="sessionId" />
    <QrCodeDisplay :sessionId="sessionId" />
  </div>
</template>

<script>
import SessionForm from '../components/SessionForm.vue'
import QrCodeDisplay from '../components/QrCodeDisplay.vue'

export default {
  name: 'SessionView',
  components: {
    SessionForm,
    QrCodeDisplay
  },
  props: {
    id: {
      type: String,
      required: true
    }
  },
  computed: {
    sessionId() {
      return this.id
    }
  }
}
</script>
