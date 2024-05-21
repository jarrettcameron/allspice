<script setup>
import { Recipe } from '../models/Recipe.js';


defineProps({ recipe: { type: Recipe, required: true}})

</script>


<template>
    <div class="">
        <div class="card border-0 rounded shadow position-relative" :style="`background: linear-gradient(0deg, rgba(0, 0, 0, 0.7) 3%, transparent 14%), url('${recipe.img}');`">
            <div class="title ps-2 text-white">
                <h6>{{ recipe.title }}</h6>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>

.title {
    position: absolute;
    bottom: 0;
}

.card {
    aspect-ratio: 1/1;
    background-size: cover !important;
    background-position: center !important;
}

</style>