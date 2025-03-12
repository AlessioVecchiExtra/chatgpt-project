<template>
  <div ref="wordCloudContainer" class="wordcloud-container"></div>
</template>

<script>
import { ref, watch, onMounted, defineComponent } from 'vue'
import WordCloud from 'wordcloud'

export default defineComponent({
  name: 'WordCloud',
  props: {
    words: {
      type: Array,
      default: () => []
    },
    backgroundColor: {
      type: String,
      default: '#001f3f' // Navy
    },
    minFontSize: {
      type: Number,
      default: 15
    },
    maxFontSize: {
      type: Number,
      default: 60
    },
    fontFamily: {
      type: String,
      default: 'Verdana'
    }
  },
  setup(props) {
    const wordCloudContainer = ref(null)
    // Palette di colori consigliata
    const colors = ['#FFC300', '#00A8E8', '#F8F9FA', '#FF6F61', '#00C49A']

    function generateCloud() {
      if (!wordCloudContainer.value) return

      const list = computeWordFrequencies()
      WordCloud(wordCloudContainer.value, {
        list,
        gridSize: 10,
        weightFactor: (size) => (size * props.maxFontSize) / 10,
        fontFamily: props.fontFamily,
        color: () => colors[Math.floor(Math.random() * colors.length)], // Sceglie un colore random dalla palette
        backgroundColor: props.backgroundColor,
        minSize: props.minFontSize
      })
    }

    function computeWordFrequencies() {
      const freqMap = {}
      props.words.forEach(word => {
        freqMap[word] = (freqMap[word] || 0) + 1
      })
      return Object.entries(freqMap)
    }

    onMounted(() => {
      generateCloud()
    })

    watch(
      () => props.words,
      () => {
        generateCloud()
      }
    )

    return {
      wordCloudContainer
    }
  }
})
</script>

<style scoped>
.wordcloud-container {
  width: 100%;
  height: 400px;
  background-color: #001f3f;
  /* Sfondo navy */
  border-radius: 10px;
  padding: 10px;
}
</style>
