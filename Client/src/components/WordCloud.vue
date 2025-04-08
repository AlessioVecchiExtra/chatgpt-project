<template>
  <div ref="wordCloudContainer" class="wordcloud-container"></div>
</template>

<script>
import { ref, watch, onMounted, defineComponent, nextTick } from 'vue'
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
      default: 'transparent' // Navy
    },
    minFontSize: {
      type: Number,
      default: 15
    },
    maxFontSize: {
      type: Number,
      default: 40
    },
    fontFamily: {
      type: String,
      default: 'Times New Roman'
    }
  },
  setup(props) {
    const wordCloudContainer = ref(null)

    const width = ref(0)
    const height = ref(0)

    // Palette di colori consigliata
    const colors = ['#6438a4', '#44a6c2', '#d1bb7f']

    function updateCanvasSize() {
      if (wordCloudContainer.value) {
        const rect = wordCloudContainer.value.getBoundingClientRect()
        width.value = rect.width
        height.value = rect.height
      }
    }

    function measureWordWidth(word, fontSize) {
      // Creiamo un canvas temporaneo per misurare il testo
      const canvas = document.createElement('canvas')
      const context = canvas.getContext('2d')
      context.font = `${fontSize}px ${props.fontFamily}`
      return context.measureText(word).width
    }

    function adjustFontSize(word, maxSize) {
      let fontSize = maxSize
      let wordWidth = measureWordWidth(word, fontSize)

      // Riduci la dimensione se la parola è più larga del canvas
      while (wordWidth > width.value * 0.8 && fontSize > props.minFontSize) {
        fontSize -= 2 // Riduci di 2px per volta
        wordWidth = measureWordWidth(word, fontSize)
      }

      return fontSize
    }


    function generateCloud() {
      if (!wordCloudContainer.value) return

      updateCanvasSize() // Ottieni la dimensione del canvas

      //console.log("canvas size ", width.value, height.value)

      const list = computeWordFrequencies()
      const maxCount = Math.max(...list.map(item => item[1]), 1) // Frequenza massima
      const minCount = Math.min(...list.map(item => item[1]), Infinity) // Frequenza minima

      //console.log(minCount, maxCount)

      WordCloud(wordCloudContainer.value, {
        list,
        // list: list.map(([word, freq]) => [word, adjustFontSize(word, (freq / maxCount) * props.maxFontSize)]),
        gridSize: Math.round(width.value / 50),
        weightFactor: (size) => { /*console.log(Math.max((size / maxCount) * props.maxFontSize, props.minFontSize));*/ return Math.max((size / maxCount) * props.maxFontSize, props.minFontSize); }, //Math.max((size / maxCount) * props.maxFontSize, props.minFontSize),
        fontFamily: props.fontFamily,
        color: () => colors[Math.floor(Math.random() * colors.length)], // Sceglie un colore random dalla palette
        backgroundColor: props.backgroundColor,
        //maxSize: props.maxFontSize,
        //minSize: props.minFontSize,
        rotateRatio: 0,
        //rotateRatio: 0.25,
        //rotationSteps: 4,
        width: width.value,
        height: height.value,
        shape:"circle", // "square" | ""
        ellipticity: 0.5
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
      //generateCloud()
      nextTick(() => {
        updateCanvasSize() // Recupera le dimensioni dopo il rendering
        generateCloud()
      })

      // Aggiorna il canvas se la finestra viene ridimensionata
      window.addEventListener('resize', generateCloud)

    })

    watch(
      () => props.words,
      () => {
        generateCloud()
      }
    )

    return {
      wordCloudContainer,
      width,
      height
    }
  }
})
</script>

<style scoped>
.wordcloud-container {
  width: 100%;
  height: 80vh;
  background-color: transparent;
}
</style>
