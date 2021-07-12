package main

import "EvolveTechnology/toll-calculator/utils"

var tollFreeVehicles = []string{"Motorbike", "Tractor", "Emergency", "Diplomat", "Foreign", "Military"}

type Vehicle interface {
	GetType() string
}

type (
	Motorbike        struct{}
	Car              struct{}
	Tractor          struct{}
	EmergencyVehicle struct{}
	DiplomatVehicle  struct{}
	ForeignVehicle   struct{}
	MilitaryVehicle  struct{}
)

func (m Motorbike) GetType() string         { return "Motorbike" }
func (c Car) GetType() string               { return "Car" }
func (t Tractor) GetType() string           { return "Tractor" }
func (ev EmergencyVehicle) GetType() string { return "Emergency" }
func (dv DiplomatVehicle) GetType() string  { return "Diplomat" }
func (fv ForeignVehicle) GetType() string   { return "Foreign" }
func (mv MilitaryVehicle) GetType() string  { return "Military" }

func IsTollFreeVehicle(v Vehicle) bool {
	return utils.Contains(tollFreeVehicles, v.GetType())
}
