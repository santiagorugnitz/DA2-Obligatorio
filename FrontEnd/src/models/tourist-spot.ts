export class TouristSpot {

    id: number
    name: String
    description: String
    image: { id: number, name: string }
    region: { id: number, name: string }
    touristSpotCategories: { category: { id: number, name: string } }[]

}
