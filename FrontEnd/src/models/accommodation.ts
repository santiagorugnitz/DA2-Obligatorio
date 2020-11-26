import { TouristSpot } from './tourist-spot'
import {Comment} from './comment'

export class Accommodation {
  id: number
  name: string
  images: string[]
  address: string
  stars: Number
  description: string
  fee: Number
  total: Number
  available: boolean
  telephone: string
  contactInformation: string
  touristSpot: TouristSpot
  selectedImage: number = 0
  score:number
  comments:Comment[]
}

export class AccommodationDTO{
  name: string
  imageNames: string[]
  address: string
  stars: Number
  description: string
  fee: Number
  available: boolean
  telephone: string
  contactInformation: string
  touristSpotId: number
}
