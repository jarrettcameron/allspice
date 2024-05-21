<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';


const recipe = computed(() => AppState.activeRecipe)

</script>


<template>
<div v-if="recipe" class="modal fade" id="recipeModal" aria-hidden="true" aria-labelledby="exampleModalToggleLabel" tabindex="-1">
  <div class="modal-dialog modal-dialog-centered">
    <div class="d-flex w-100 p-0 flex-lg-row flex-column">
        <img :src="recipe.img" class="recipeImg p-0" alt="">
        <div class="bg-white special-round p-4 w-100">
            <div class="mb-2 d-flex align-items-center justify-content-between">
                <div class="d-flex gap-3 align-items-center">
                    <h2 class="fw-bold text-warning p-0 m-0">{{ recipe.title }}</h2>
                    <span class="poet text-primary fs-5 text-uppercase px-3 rounded tag">{{ recipe.category }}</span>
                </div>
                <button class="btn btn-close"></button>
            </div>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg col-12">
                        <h4>Recipe</h4>
                        <p></p>
                    </div>
                    <div class="col-lg col-12">
                        <h4>Instructions</h4>
                        <p>{{ recipe.instructions }}</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>
</div>
</template>


<style lang="scss" scoped>

.tag {
    background-color: #adbcf7;
}

.special-round {
    border-top-right-radius: 0.375rem;
    border-bottom-right-radius: 0.375rem;
}

.modal-dialog {
    max-width: 80vw;
}

.recipeImg {
    border-top-left-radius: 0.375rem;
    border-bottom-left-radius: 0.375rem;
    object-fit: cover;
    max-width: 30vw;
    height: 80vh;
}

@media screen and (max-width: 992px) {

    .special-round {
        border-top-right-radius: 0rem;
        border-bottom-right-radius: 0.375rem;
        border-bottom-left-radius: 0.375rem;
    }

    .modal-dialog {
        max-width: 85vw;
    }

    .recipeImg {
        border-top-left-radius: 0.375rem;
        border-top-right-radius: 0.375rem;
        border-bottom-left-radius: 0rem;
        width: 100%;
        max-width: 100%;
        max-height: 30vh;
    }
}

@media screen and (max-width: 576px) {
    .modal-dialog {
        max-width: 100vw;
    }
}
</style>