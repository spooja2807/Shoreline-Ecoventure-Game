using UnityEngine;



public class Animal_Rescue : MonoBehaviour

{

    private GameObject nearbyAnimal = null;

    public WordPuzzle wordPuzzle;



    void Update()

    {

        if (nearbyAnimal != null && Input.GetKeyDown(KeyCode.S))

        {

            AnimalTag tag = nearbyAnimal.GetComponent<AnimalTag>();

            if (tag != null)

            {

                wordPuzzle.StartWordPuzzle(nearbyAnimal, tag.animalIndex);

                wordPuzzle.ShowRescuePrompt(false); // Hide the prompt after rescue starts

            }

        }

    }



    void OnTriggerEnter2D(Collider2D collision)

    {

        if (collision.CompareTag("Animal"))

        {

            nearbyAnimal = collision.gameObject;

            wordPuzzle.ShowRescuePrompt(true); // Show "Press S to Rescue" message

        }

    }



    void OnTriggerExit2D(Collider2D collision)

    {

        if (collision.CompareTag("Animal") && collision.gameObject == nearbyAnimal)

        {

            nearbyAnimal = null;

            wordPuzzle.ShowRescuePrompt(false); // Hide prompt when leaving animal

        }

    }

}