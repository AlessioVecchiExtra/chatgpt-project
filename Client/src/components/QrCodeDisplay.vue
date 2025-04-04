<template>
  <div class="qr-code">
    <qrcode-vue :value="qrValue" :width="250" :height="250" :dotsOptions="{
            type: 'square',
            color: '#0C1E3B',
          }"
          :cornersSquareOptions="{ type: 'square', color: '#8CC63F' }"
          :cornersDotOptions="{ type: 'square', color: '#0C1E3B' }"/>
          <!-- <p>bit.ly/CATchUP</p> -->
  </div>
</template>

<script>
import { computed, defineComponent } from 'vue'
import QrcodeVue from 'qrcode-vue3'

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
