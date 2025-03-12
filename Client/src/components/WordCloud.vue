<template>
  <div ref="wordCloudContainer" style="width: 500px; height: 500px;"></div>
</template>

<script>
import WordCloud from 'wordcloud'
import { ref, watch, onMounted, defineComponent } from 'vue'

export default defineComponent({
  name: 'WordCloud',
  props: {
    words: {
      type: Array,
      default: () => []
    }
  },
  setup(props) {
    const wordCloudContainer = ref(null)

    function generateCloud() {
      const list = computeWordFrequencies()
      WordCloud(wordCloudContainer.value, {
        list,
        weightFactor: 3
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
      wordCloudContainer,
      generateCloud,
      computeWordFrequencies
    }
  }
})
</script>
