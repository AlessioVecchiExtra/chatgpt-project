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
    path: '/session/:sessionId',
    name: 'Session',
    component: SessionView,
    props: true
  },
  {
    path: '/thankyou/:sessionId',
    name: 'ThankYou',
    component: ThankYouView,   
    props: true
  },
  {
    path: '/results',
    name: 'Results',
    component: ResultsView    
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
