<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-light bg-white px-3 d-flex justify-content-between">
    <div style="min-width: 5.6vw;">

    </div>
    <router-link class="navbar-brand p-0 m-0 poet" :to="{ name: 'Home' }">
        <div class="d-flex gap-1 fs-2">
            <i class="text-warning mdi mdi-silverware-spoon"></i>
            <span class="text-black">AllSpice</span>
        </div>
    </router-link>
    <button class="navbar-toggler border-0" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse flex-grow-0" style="min-width: 5.6vw;" id="navbarText">
        <div class="position-relative">
            <div class="position-md-absolute" style="left: 0%; bottom: -20px;">
                <Login />
            </div>
        </div>
      <!-- LOGIN COMPONENT HERE -->
    </div>
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
