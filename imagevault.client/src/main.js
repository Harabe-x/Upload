import axios from "axios";
import './app.css'
import App from './App.svelte'



//axios defaults

axios.defaults.baseURL = 'https://localhost:10000/api';



const app = new App({
  target: document.getElementById('app'),
})

export default app












