<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { Modal } from 'bootstrap';


const account = computed(() => AppState.account)
const recipes = computed(() => AppState.recipes)

async function getRecipes() {
    try {
        await recipesService.getRecipes()
    }
    catch (error){
        console.error(error)
      Pop.error(error);
    }
}

function openModal(recipe)
{
    AppState.activeRecipe = recipe
    Modal.getOrCreateInstance('#recipeModal').show()
}

onMounted(() => {
    getRecipes()
})

</script>

<template>
    <div class="container-fluid g-0">
        <div class="row g-0">
            <div class="col-12 hero position-relative">
                <div class="hero-card text-center">
                    <h2 class="d-md-block d-none poet text-white">Discover, Share and Savor.<br>Your Ultimate Destination for Culinary Inspiration.</h2>
                    <h2 class="d-md-none d-block poet text-white">Your Ultimate Destination<br>for Culinary Inspiration.</h2>
                </div>
            </div>
        </div>
        <div v-if="account" class="mt-3 row justify-content-center g-0">
            <div class="col-lg-5 col-xl-4 col-md-7 col-11 d-flex justify-content-center gap-3 fw-bold">
                <router-link :to="{ name: 'Home' }" class="border-1 border-bottom border-black border-2 text-black px-4 py-1">
                    Home
                </router-link>
                <router-link :to="{ name: 'Home' }" class="border-1 border-bottom border-black border-2 text-black px-4 py-1">
                    My Recipes
                </router-link>
                <router-link :to="{ name: 'Home' }" class="border-1 border-bottom border-black border-2 text-black px-4 py-1">
                    Favorites
                </router-link>
            </div>
        </div>
        <div class="mt-4 row justify-content-center g-0">
            <div class="col-xxl-7 col-sm-10 col-12">
                <div class="p-2 row g-0">
                    <div v-for="recipe in recipes" :key="recipe.id" class="px-1 col-md-4 col-6 mb-2">
                        <RecipeCard @click="openModal(recipe)" :recipe="recipe" class="pointer"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">

.hero-card {
    width: 100%;
    position: absolute;
    bottom: 6em;
    h2 {
        text-shadow: 3px 3px 0px rgba(0, 0, 0, 0.531);
    }
}

.hero {
    height: 40vh;
    background-image: url('https://images.unsplash.com/photo-1627488193141-953623010488?q=80&w=2940&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D');
    background-size: cover;
    background-position: center;
}


@media screen and (max-width: 1300px) {
  .hero {
    height: 20vh;
  }

  .hero-card {
    bottom: 4em;
  }
}
</style>
