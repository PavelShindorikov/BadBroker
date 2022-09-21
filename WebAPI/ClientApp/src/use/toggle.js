import { ref } from "vue";
export const useToggle = () => {
    const visible = ref(false);
    const toggle = () => visible.value = !visible.value;
    return { visible, toggle };
};
