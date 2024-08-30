import axios from "axios";
import './app.css'
import App from './App.svelte'



//axios defaults

axios.defaults.baseURL = 'http://localhost:10000/api/v1/';



const app = new App({
  target: document.getElementById('app'),
})
export default app












