package utils

import (
	"testing"
)

func TestContains(t *testing.T) {
	list := []string{"hi"}
	t.Run("item=\"hi\"", func(t *testing.T) {
		got := Contains(list, "hi")
		if !got {
			t.Errorf("Contains(\"hi\") = %t; want true", got)
		}
	})
	t.Run("item=\"bye\"", func(t *testing.T) {
		got := Contains(list, "bye")
		if got {
			t.Errorf("Contains(\"hi\") = %t; want false", got)
		}
	})
}
