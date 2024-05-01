import { writable } from "svelte/store";
// @ts-ignore
import DashboardPage from '../../lib/Pages/Dashboard/DashboardPage.svelte'

const currentPageStore = writable(DashboardPage)

export function getNavigationStore()
{
    return currentPageStore;
}
