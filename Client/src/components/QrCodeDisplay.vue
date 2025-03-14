<template>
  <div>
    <p>Scansiona questo QR code per votare da smartphone:</p>
    <qrcode-vue :value="qrValue" :size="200" />
  </div>
</template>

<script>
import { computed, defineComponent } from 'vue'
import QrcodeVue from 'qrcode.vue'

export default defineComponent({
  name: 'QrCodeDisplay',
  components: {
    QrcodeVue
  },
  props: {
    sessionId: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const qrValue = computed(() => {
      return `${window.location.origin}/session/${props.sessionId}`
    })

    return {
      qrValue
    }
  }
})
</script>
