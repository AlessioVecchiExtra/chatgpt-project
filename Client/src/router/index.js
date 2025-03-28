import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import SessionView from '../views/SessionView.vue'
import ResultsView from '../views/ResultsView.vue'
import ThankYouView from '../views/ThankYouView.vue'


const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomeView
  },
  {
    path: '/session/:id',
    name: 'Session',
    component: SessionView,
    props: true
  },
  {
    path: '/thankyou',
    name: 'ThankYou',
    component: ThankYouView,   
  },
  {
    path: '/results/:sessionId?',
    name: 'Results',
    component: ResultsView,
    props: true
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
